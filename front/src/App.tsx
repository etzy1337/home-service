import React from 'react';
import CardList from './Components/CardList/CardList';
import { Outlet } from 'react-router-dom';
import "react-toastify/dist/ReactToastify.css";
import { ToastContainer } from 'react-toastify';
import { UserProvider } from './Context/useAuth';
import Navbar from './Components/Navbar/Navbar';


function App() {
  return (
   <div>
    <UserProvider>
    <Navbar/>
    <Outlet/>
    <ToastContainer/>
    </UserProvider>
    </div>
  );
}

export default App;
