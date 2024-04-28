import React, { useState } from 'react'
import GoogleMap from './GoogleMap'

export default function AdressMapSearch({ onViewChanged }) {

    const [location, setLocation] = useState(null);

    const handleAdressObtained = (location) => {
        setLocation(location);
    }

    return (
        <div className='adress-map-container'>
            <h1>Localizeaz/a adresa ta pe hart/a</h1>
            <div className='adress-map-map'>
                <GoogleMap onAddressObtained={handleAdressObtained} />

            </div>
            <div className='adress-map-result'>
                <img src='/map.png' />
                <p>{location?.adress}</p>
            </div>
            <button onClick={() => onViewChanged(location)}>Confirma adresa</button>
        </div>
    )
}

