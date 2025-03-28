import React from 'react'
import CardList from '../../Components/CardList/CardList'
import AddCardModal from '../../Components/AddCardModal/AddCardModal'
import { useAuth } from '../../Context/useAuth';



type Props = {}

const HomePage = (props: Props) => {
  const {isAdmin}=useAuth();
  return (
    <div>
        <h1>Dostepne uslugi</h1>
        <CardList/>
        {isAdmin() ? (<AddCardModal/>):("")}
        
    </div>
  )
}

export default HomePage