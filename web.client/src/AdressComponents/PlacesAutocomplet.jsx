"use client";

import React, { useState } from 'react'
import { useLoadScript } from "@react-google-maps/api";

import usePlacesAutocomplete, {
    getGeocode,
    getLatLng,
} from "use-places-autocomplete";
import FloatingInput from '../Components/FloatingInput';


export default function PlacesAutocomplet() {

    const [isFocused, setIsFocused] = useState(false);

    const { isLoaded } = useLoadScript({
        googleMapsApiKey: "your_api_key_here",
        libraries: ["places"],
    });

    const {
        ready,
        value,
        setValue,
        suggestions: { status, data },
        clearSuggestions,
    } = usePlacesAutocomplete();

    if (!isLoaded) return <div>Loading...</div>;

    const handleSelect = async (address) => {
        console.log("selected value", address);

        setValue(address, false);
        clearSuggestions();

        const results = await getGeocode({ address });
        const { lat, lng } = await getLatLng(results[0]);
        setSelected({ lat, lng });
    };
    // https://stackoverflow.com/questions/62244079/how-to-load-the-google-places-api-in-react
    // https://www.youtube.com/watch?v=BL2XVTqz9Ek
    return (
        <div className="search-container">
            <div onClick={() => setIsFocused(true)}>
                <FloatingInput label={"Search street"} onValueChange={setValue} predefinedValue={value} />
            </div>
            {<ul className="results">
                {status === "OK" &&
                    data.map(({ place_id, description }) => (
                        <li key={place_id} onMouseDown={() => {
                            handleSelect(description);
                            setIsFocused(false);
                        }}>
                            {description}
                        </li>
                    ))}
            </ul>}
        </div>
    );
}
