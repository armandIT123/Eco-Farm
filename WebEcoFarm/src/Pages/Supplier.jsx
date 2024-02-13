import React, { useEffect, useState } from "react";
import { useParams } from 'react-router-dom';


export default function Supplier() {

    const [supplier, setSupplier] = useState(null);
    const [products, setProducts] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const { id } = useParams();

    const fetchSupplier = async () => {
        try {
            const response = await fetch(`https://localhost:7184/Supplier`);
            const data = await response.json();
            setSuppliers(data);
            setError(null);
        } catch (error) {
            if (error.name === "AbortError") {
                console.log("Fetch aborted");
                return;
            }
            console.log(error.message);
            setError(error.message);
        } finally {
            setIsLoading(false);
        }
    };

    const fetchProducts = async () => {
        try {
            const response = await fetch('https://localhost:7184/Product?supplierId=' + id);
            const data = await response.json();
            console.log(data);
            setProducts(data);
            console.log(products);
            setError(null);
        } catch (error) {
            console.log(error);
        } finally {
            setIsLoading(false);
        }
    };

    useEffect(() => {
        fetchProducts();
    }, []);

    return (
        <>
            <h1>Producator</h1>
        </>
    )
};
