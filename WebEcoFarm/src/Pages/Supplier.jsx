import { useEffect, useState } from "react";
import { useParams } from 'react-router-dom';

import Spinner from 'react-bootstrap/Spinner';
import ProductCard from "../Components/ProductCard";
import ProductModal from "../Components/ProductModal";
import CartPreview from "../Components/CartPreview";


export default function Supplier() {

    const [products, setProducts] = useState([]);

    const [supplier, setSupplier] = useState(null);
    const [description, setDescription] = useState(null);
    const [error, setError] = useState(null);

    const [isLoading, setIsLoading] = useState(true);

    const [clickedProductIndex, setClickedProductIndex] = useState(-1);
    const { slug, id } = useParams();

    const fetchSupplier = async () => {
        try {
            const response = await fetch(`https://localhost:7184/Supplier/GetSupplier?supplierId=` + id);
            if (!response.ok)
                throw new Error(`Error: ${response.status}`);
            const data = await response.json();
            setSupplier(data);
            fetchProducts();
            setError(null);
        }
        catch (error) {
            /*  console.log(error.message); */
            setError(error.message);
        }
        finally {
            setIsLoading(false);
        }
    };

    const fetchProducts = async () => {
        try {
            const response = await fetch('https://localhost:7184/Product?supplierId=' + id);
            if (!response.ok)
                throw new Error(`Error: ${response.status}`);
            const data = await response.json();
            setProducts(data);
            setError(null);
        }
        catch (error) {
            /*console.log(error.message);*/
        }
        finally {
            setIsLoading(false);
        }
    };

    //get supplier from redux store
    useEffect(() => {
        fetchSupplier();
    }, []);

    return (
        <>
            {isLoading ? <Spinner animation="border" /> :
                <div className="supplier-page-container">
                    { // Top Part
                        supplier &&
                        <div className="supplier-top-container">
                            <img src={"data:image/jpeg;base64," + supplier.image} />
                            <div className="supplier-white-board">
                                <h2>{supplier.name}</h2>
                                <p>asdasdasdasdasd ads ASd A dasd AS dASd as DASD a dasd ASD ASD sd dasd asd </p>
                                <p>asdasdasdasdasd ads ASd A dasd AS dASd as DASD a dasd ASD ASD sd dasd asd </p>
                            </div>
                        </div>
                    }

                    <div className="products-container">
                        {products.map((product, index) => (
                            <ProductCard key={index}
                                product={product}
                                onClick={() => setClickedProductIndex(index)} />
                        ))}
                    </div>
                    <CartPreview className='supplier-cart-preview' />
                </div>
            }


            {clickedProductIndex > -1 && <ProductModal
                show={clickedProductIndex > -1}
                onHide={() => setClickedProductIndex(-1)}
                product={products[clickedProductIndex]} />}
        </>
    )
};
