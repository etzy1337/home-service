import React from 'react'
import "./MyReservationCard.css"
import { MyReservationsGet } from '../../Models/Reservation';
import { deleteReservationApi } from '../../Api/ReservationService';
import { toast } from 'react-toastify';
import { useAuth } from '../../Context/useAuth';
interface Props  {
    reservation:MyReservationsGet
}

const MyReservationCard = ({reservation}: Props) => {
    
    const formattedDate = new Date(reservation.date);

    formattedDate.setHours(formattedDate.getHours() + 4);

    const formattedDateString = formattedDate.toLocaleDateString('pl-PL', {
        day: '2-digit',
        month: 'long',
        year: 'numeric',
    });

    const formattedTimeString = formattedDate.toLocaleTimeString('pl-PL', {
        hour: '2-digit',
        minute: '2-digit',
    });


    const handleDelete = async()=>{
        try{
            await deleteReservationApi(reservation.id);
            toast.success("Reservation deleted successfully!")
        }catch(err:any){
            toast.warning("Failed to delete",err);
        }

    }

    const {isLoggedIn}=useAuth();
    return (
        <div className="card">
          <h2 className="card-title">{reservation.serviceName}</h2>
          <p className="card-text">📍 {reservation.address}</p>
          <p className="card-text">📅 {formattedDateString}, {formattedTimeString}</p>
          {isLoggedIn() ? (
          <button onClick={handleDelete}>Delete reservation</button>
        ): ("")}
        </div>
        
      );
}

export default MyReservationCard