import { OffersGet, OffersPost } from "../Models/Offers"
import axios from "axios";

export const getOfferApi = async()=>{
    try{
        const response = await axios.get<OffersGet[]>("http://localhost:5173/api/service");
        return response.data;
    }catch(err){
        console.error("Error fetching offers:", err);
        return null;
    }
}

export const postOfferApi = async (offer: OffersPost) => {
    try {
        const response = await axios.post("http://localhost:5173/api/service", offer);
        return response.data; 
    } catch (err) {
        console.error("Error posting offer:", err);
        return null; 
    }
}

export const deleteOfferApi = async(id:number)=>{
    try{
        const response = await axios.delete(`http://localhost:5173/api/service`,{
            params:{id}
        });
        return response.data;
    }catch(err){
        console.error("deleteOfferApi Error");
        throw err;
    }
}