import React, { useEffect, useState } from 'react';
import DatePicker from 'react-datepicker';
import "react-datepicker/dist/react-datepicker.css";
import { toast } from 'react-toastify';
import { getReservatedDateApi, postReservationApi } from '../../Api/ReservationService';
import { useParams } from 'react-router-dom';
import { parseISO } from 'date-fns';

type Props = {};

const ReservePage = (props: Props) => {
  const [selectedDate, setSelectedDate] = useState<Date | null>(null);
  const[address,setAddress]=useState<string>("");
  const {serviceId}=useParams<{serviceId:string}>();
  const[reservatedDate,setReservatedDate]=useState<{date: string}[]>([])
  const serviceIdNumber = Number(serviceId);
  

  const handleDateChange = (date: Date | null) => {
    setSelectedDate(date);
  };
  

useEffect(()=>{
  const GetData= async()=>{
    try{
      const response = await getReservatedDateApi(serviceIdNumber)
      if(response){setReservatedDate(response);}
      
  }catch(err){
      console.log("GetReservatedDate error",err)
  }
  }
  GetData();
},[serviceIdNumber])
console.log(reservatedDate)
  const handleAddressChange =(event:any)=>{
    setAddress(event.target.value);
  }

  const handleReservation = async(event:any)=>{
    event.preventDefault();
    if(!selectedDate||!address) {toast.warning("Fill data");return;}
    const formattedDate = selectedDate.toISOString().slice(0, 19);

    var resObj ={
      date:formattedDate,
      address:address
    }
    try{
      postReservationApi(serviceIdNumber,resObj)
      toast.success("Reservation successfully added!")
    }catch(err){
      toast.warning("Something went wrong");
    }
  }
  


  const formatDate = (date: Date | null) => {
    if (!date) return 'No date selected';
    return date.toLocaleString('pl-PL', { // Ustawiamy lokalizację na Polskę
      weekday: 'long',  // Dzień tygodnia
      year: 'numeric',
      month: 'long',    // Pełna nazwa miesiąca
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
      hour12: false,    // 24-godzinny format
    });
  };

  // Funkcja do generowania listy godzin dla danego dnia
  const getExcludedTimesForDate = (date: Date) => {
    const excludedTimes: Date[] = [];

    // Iteracja po zarezerwowanych dniach i godzinach
    reservedDate.forEach((reservedDate) => {
      if (reservedDate.toDateString() === date.toDateString()) {
      

        const plusFourHours = new Date(reservedDate);
      plusFourHours.setHours(plusFourHours.getHours() + 4);
      excludedTimes.push(plusFourHours);
      }
    });

    return excludedTimes;
  };

  const [excludeTimes, setExcludeTimes] = useState<Date[] | undefined>(undefined);

  // Zmiana wykluczonych godzin po wybraniu daty
  useEffect(() => {
    if (selectedDate) {
      const timesToExclude = getExcludedTimesForDate(selectedDate);
      setExcludeTimes(timesToExclude);
    }
  }, [selectedDate]);

  

  const reservedDate = reservatedDate.map(item => 
    {
      return parseISO(item.date);
    })

  return (
    <div>
      <h1>Reserve Page</h1>
      <form onSubmit={handleReservation}>

      <label htmlFor="address">Enter Address:</label>
        <input
          type="text"
          id="address"
          value={address}
          onChange={handleAddressChange}
          placeholder="Enter address"
        />



        <label htmlFor="reservationDateTime">Choose a date and time:</label>
        <DatePicker
          selected={selectedDate}
          onChange={handleDateChange}
          showTimeSelect
          dateFormat="yyyy-MM-ddTHH:mm"
          timeFormat="HH:mm"
          timeIntervals={120} 
          minDate={new Date()}
          maxDate={new Date('2025-12-31')}
          placeholderText="Click to select date and time"
          excludeTimes={excludeTimes}
        />
        <p>Selected date and time: {formatDate(selectedDate)}</p>

        <button type="submit">Make reservation</button>
      </form>
    </div>
  );
};

export default ReservePage;
