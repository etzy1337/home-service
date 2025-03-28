import React, { JSX, useEffect, useState } from 'react'
import { getMyReservationsApi } from '../../Api/ReservationService'
import { MyReservationsGet } from '../../Models/Reservation'
import MyReservationCard from '../MyReservationCard/MyReservationCard';

type Props = {}





const MyReservationCardList:React.FC<Props> = (props: Props):JSX.Element => {
    const[reservations,setReservations]=useState<MyReservationsGet[]>([]);
    useEffect (()=>{
        const getData = async()=>{
            const value = await getMyReservationsApi();
            if(value){
                setReservations(value)}
                
        }
        getData();
    },[])
    console.log(reservations);
  return (
    <div>

        {reservations.map((reservation)=>(
            <MyReservationCard key={reservation.id} reservation={reservation}/>
        ))}

    </div>
  )
}

export default MyReservationCardList