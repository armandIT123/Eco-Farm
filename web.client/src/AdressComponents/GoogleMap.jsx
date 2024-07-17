"use client";

import { useEffect, useState } from "react";
import {
    APIProvider,
    Map,
    AdvancedMarker,
    Pin,
    useMap,
    useMapsLibrary
} from "@vis.gl/react-google-maps";


export default function GoogleMap({ onAddressObtained, options, defaultCenter = { lat: 44.4268, lng: 26.1025 } }) {

    const [mapRef, setMapRef] = useState(null);
    const [center1, setCenter] = useState();
    const handleUseMap = (map) => {
        setMapRef(map);
    }

    const handleCenterChanged = () => {
        setCenter(mapRef?.center);
    }

    const handleAdressObtained = (adress) => {
        if (onAddressObtained)
            onAddressObtained({ adress, center1 });
    }

    useEffect(() => {
        handleCenterChanged();
    }, [mapRef])

    return (
        <APIProvider apiKey="AIzaSyAq2hy9icVrPBP2b2G73V8FFMG5rCZ6bXU">
            <div style={{ height: "50vh", width: "100%" }}>
                <Map
                    defaultZoom={15}
                    mapId="c4b23e8676a87a85"
                    defaultCenter={defaultCenter}
                    fullscreenControl={false}
                    onCenterChanged={handleCenterChanged}
                    disableDefaultUI={true}
                    options={options}
                >
                    <Geocoder onUseMap={handleUseMap} coordinates={center1} onAddressObtained={handleAdressObtained} />
                    <AdvancedMarker position={center1}>
                        <Pin
                            background={"grey"}
                            borderColor={"green"}
                            glyphColor={"purple"}
                        />
                    </AdvancedMarker>
                </Map>
            </div>
        </APIProvider>
    );
}

const Geocoder = ({ coordinates, onAddressObtained, onUseMap }) => {
    const map = useMap();
    const geocodingApiLoaded = useMapsLibrary("geocoding");

    const [geocodingService, setGeocodingService] = useState();
    const [geocodingResult, setGeocodingResult] = useState();
    const [adress, setAdress] = useState("");

    useEffect(() => {
        if (!geocodingApiLoaded) return;
        setGeocodingService(new window.google.maps.Geocoder());
    }, [geocodingApiLoaded])

    useEffect(() => {

        if (!geocodingService) return;


        const delayDebounceFn = setTimeout(() => {

            geocodingService.geocode({ location: coordinates }, (results, status) => {
                if (status === "OK") {
                    if (results[0]) {
                        onAddressObtained(results[0].formatted_address);
                    } else {
                        console.log("No results found");
                    }
                } else {
                    console.error("Geocoder failed due to: " + status);
                }
            })
        }, 1000)

        return () => clearTimeout(delayDebounceFn)

    }, [geocodingService, coordinates])

    useEffect(() => {
        if (!map) return;
        onUseMap(map);
    }, [map]);

    return <>...</>;
};