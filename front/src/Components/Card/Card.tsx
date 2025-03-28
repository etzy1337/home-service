import React, { JSX } from 'react'
import { Link } from 'react-router-dom';
import "./Card.css"
import { deleteOfferApi } from '../../Api/OffersService';
import { toast } from 'react-toastify';
import { useAuth } from '../../Context/useAuth';

interface Props  {
    id:number;
    name:string
}



const Card:React.FC<Props> = ({id,name}: Props):JSX.Element => {
  const {isAdmin}=useAuth();
const handleDelete=async()=>{
  try{
    await deleteOfferApi(id);
    window.location.reload();
    toast.success("Offer deleted successfully");
  }catch(err:any){
    toast.warning("Failed to delete: ",err)
    console.log(id);
  }
}

  return (
    <div className='card'>{name}
    <Link to={`/order-page/${id}`}className='button'>
    <span>order service</span>
    </Link>

    {isAdmin()?(
    <button onClick={handleDelete} className="button delete-button">
        <span>Delete Service</span>
    </button>
    ):("")
    }

    </div>
  )
}

export default Card