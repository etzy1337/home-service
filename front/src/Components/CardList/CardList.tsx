import React, { JSX, useEffect, useState } from 'react'
import Card from '../Card/Card';
import { OffersGet } from '../../Models/Offers';
import { getOfferApi } from '../../Api/OffersService';

interface Props  {
}


const CardList:React.FC<Props> = (props: Props):JSX.Element => {
  const[offer,setOffer] = useState<OffersGet[]>([]);

  useEffect(()=>{
    const getData = async()=>{
      const value = await getOfferApi()
      if(value){
        setOffer(value);
      }
    }
    getData();
  },[])
  return (
    <div>

    {offer.map((offer)=>(
      <Card key={offer.id} id={offer.id} name={offer.name}/>
    ))}

    </div>
  )
}

export default CardList