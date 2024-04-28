import React, { useEffect, useState } from 'react'
import FloatingInput from '../Components/FloatingInput'
import GoogleMap from './GoogleMap';
import PlacesAutocomplet from './PlacesAutocomplet';



export default function AdressManualSearch({ onViewChanged, onSaveDetails, currentLocation }) {

    const mapOptions = {
        gestureHandling: 'none',
        zoomControl: false,
        mapTypeControl: false,
        streetViewControl: false,
        rotateControl: false,
        fullscreenControl: false,
        panControl: false,
        clickableIcons: false,
        keyboardShortcuts: false,
        scrollwheel: false,
    };

    const [location, setLocation] = useState(currentLocation);
    const [additional, setAdditional] = useState("");

    const handleAdressChanged = (value) => {
        setLocation(value);
    }

    const handleAdditionalChanged = (value) => {
        setAdditional(value);
    }

    const handleConfirmBtn = () => {
        if (location) {
            onSaveDetails(location, additional);
        }
    }

    useEffect(() => {
        if (currentLocation) {
            setLocation(currentLocation);
        }
    }, [currentLocation])

    return (
        <div className='adress-manual-container'>
            <h1>Adaug/a o adresa de livrare</h1>
            <div className='adress-manual-inputs'>
                <PlacesAutocomplet />
                <FloatingInput label={"Caut/a strada"} onValueChange={handleAdressChanged} predefinedValue={location?.adress} />
                <FloatingInput label={"Etaj, Apartament, instructiuni"} onValueChange={handleAdditionalChanged} />
                <p onClick={() => onViewChanged("")}>Sau indic/a locatia pe harta</p>
            </div>
            <div className='adress-manual-map-preview'>
                <GoogleMap options={mapOptions} defaultCenter={location?.center1} />
            </div>

            <button onClick={() => handleConfirmBtn()}>Confirm/a adresa</button>
        </div>
    )
}

