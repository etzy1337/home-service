import axios from "axios";
import {MyReservationsGet, ReservationPost } from "../Models/Reservation";



export const postReservationApi = async (serviceId:number,reservationPost:ReservationPost)=>{
  try{
    const response =await axios.post<ReservationPost>
    (`http://localhost:5173/api/reservation/${serviceId}`,
      reservationPost
    );
    return response.data;
  }catch(error){
    console.error("Post reservation error",error);
    throw error;
  }
}



export const getReservatedDateApi = async(serviceId:number)=>{
  try{
    const response = await axios.get<{date: string}[]>(`http://localhost:5173/api/reservation/${serviceId}`)
    return response.data;
  }catch(err){
    console.error("GetReservatedApi error",err);
    throw err;
    }
 }


export const getMyReservationsApi = async()=>{
  try{
  const response = await axios.get<MyReservationsGet[]>(`http://localhost:5173/api/reservation/user`)
  return response.data;
  }catch(err){
    console.log("MyReservationsError");
    throw err;
  }
}

export const deleteReservationApi = async(id:number)=>{
  try{
    const response = await axios.delete(`http://localhost:5173/api/reservation`,{
      params:{id}
    })
    return response.data;
  }catch(err){
    console.error("DeleteReservationError");
    throw err;
  }
}