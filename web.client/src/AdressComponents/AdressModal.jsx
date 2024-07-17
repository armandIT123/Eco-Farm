import React, { useState } from 'react'
import { Modal } from 'react-bootstrap'
import AdressManualSearch from './AdressManualSearch'
import AdressMapSearch from './AdressMapSearch';

export default function AdressModal(props) {

    const [manualSearch, setManualSearch] = useState(true);
    const [location, setLocation] = useState(null);

    const handleViewChanged = (location) => {
        if (location !== null)
            setLocation(location);
        setManualSearch(!manualSearch);
    }

    const handleSaveAdress = (location, additional) => {

    }

    return (
        <Modal
            {...props}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered
            scrollable={true}
        >
            <Modal.Body className='modal-body' >
                {
                    manualSearch ?
                        <AdressManualSearch onViewChanged={handleViewChanged} currentLocation={location} onSaveDetails={handleSaveAdress} />
                        :
                        <AdressMapSearch onViewChanged={handleViewChanged} />
                }
            </Modal.Body>
        </Modal>
    )
}

