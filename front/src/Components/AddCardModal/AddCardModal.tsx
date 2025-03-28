import React, { useState } from 'react';
import './AddCardModal.css'; // Dodajemy import CSS dla stylów
import { postOfferApi } from '../../Api/OffersService';

const Modal: React.FC = () => {
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [name, setName] = useState<string>(''); // Stan do trzymania nazwy oferty

  // Funkcja otwierająca modal
  const openModal = () => {
    setIsModalOpen(true);
  };

  // Funkcja zamykająca modal
  const closeModal = () => {
    setIsModalOpen(false);
    setName(''); // Resetowanie nazwy po zamknięciu
  };

  const handleAddOffer = async ()=>{
    if(name.trim()===''){
        alert('Please enter a valid name');
        return;
    }

    const newOffer={
        name:name
    };

    const response = await postOfferApi(newOffer);

    if(response){
        alert('Offer added successfully')
        setName('');
        closeModal();
        window.location.reload();
    }else{
        alert('Failed to add offer');
    }
  }

  return (
    <div>
      {/* Przycisk do otwierania modalu */}
      <button onClick={openModal}>+</button>

      {/* Modal */}
      {isModalOpen && (
        <div className="modal">
          <div className="modal-content">
            <h2>Add New Offer</h2>
            <input
              type="text"
              value={name}
              onChange={(e) => setName(e.target.value)}
              placeholder="Enter offer name"
            />
            <div>
              <button onClick={closeModal}>Cancel</button>
              <button onClick={handleAddOffer}>
              Add Offer
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default Modal;
