import { useState, useEffect } from 'react';

import CardItem from './CardItem';
import Spinner from 'react-bootstrap/Spinner';

function createSlug(name) {
    return name
        .toLowerCase() // Convert to lowercase
        .trim() // Trim whitespace from both ends
        .replace(/[^a-z0-9]+/g, '-') // Replace non-alphanumeric chars with '-'
        .replace(/^-+|-+$/g, ''); // Remove leading and trailing '-'
}

function Cards() {

    const [suppliers, setSuppliers] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);

    const fetchUsingAsyncAwaitWithFetchApi = async () => {
        try {
            const response = await fetch(`https://localhost:7184/Supplier`); // Fetch data based on the current page
            const data = await response.json();
            setSuppliers(data);
            setError(null);
        } catch (error) {
            if (error.name === "AbortError") {
                console.log("Fetch aborted"); // Log a message when the request is intentionally aborted
                return; // Exit the function to prevent further error handling
            }
            console.log(error.message);
            setError(error.message);
        } finally {
            setIsLoading(false);
        }
    };

    useEffect(() => {
        fetchUsingAsyncAwaitWithFetchApi();

        /*return () => {
            abortController.abort(); // Cancel any ongoing requests
            setIsLoading(true); // Reset loading state
        };*/

    }, [])

    /*https://medium.com/@itsanuragjoshi/fetching-data-from-apis-in-react-best-practices-and-methods-e959e92206f4*/
    return (
        <>
            <div className='cards'>
                {isLoading && <Spinner animation="border" />}

                <div className='cards__container'>
                    <div className='cards__wrapper'>
                        <ul className='cards__items'>
                            {suppliers.map((supplier, index) => (
                                <CardItem key={index}
                                    src={"data:image/jpeg;base64," + supplier.image}
                                    text={supplier.name}
                                    label={supplier.id}
                                    path={"/descopera/" + createSlug(supplier.name) + "/" + supplier.id}
                                />
                            ))}
                        </ul>
                    </div>
                </div>
            </div>
        </>
    );
}

export default Cards;