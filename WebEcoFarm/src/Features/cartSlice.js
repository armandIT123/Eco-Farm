import { createSlice } from "@reduxjs/toolkit";


const initialState = {
    cartItems: localStorage.getItem('cartItems1') !== null ? JSON.parse(localStorage.getItem('cartItems1')) : [],
}

const cartSlice = createSlice({
    name: "cart",
    initialState,
    reducers: {
        addToCart(state, action){
            const itemIdex = state.cartItems.findIndex((item)=>item.id === action.payload.id);
            if(itemIdex >= 0)
            {
                state.cartItems[itemIdex].quantity = action.payload.quantity;
            }
            else
            {
                if(state.cartItems.length > 0 && state.cartItems[0].supplierId !== action.payload.supplierId)
                    return;
                state.cartItems.push(action.payload);
            }
            localStorage.setItem('cartItems1', JSON.stringify(state.cartItems));
        },
        changeQuantity(state, action){
            const itemIdex = state.cartItems.findIndex((item)=>item.id === action.payload.id);
            if(itemIdex >= 0)
                state.cartItems[itemIdex].quantity += action.payload.quantity;
        },
        removeFromCart(state, action) {
            const itemId = action.payload;
            state.cartItems = state.cartItems.filter(item => item.id !== itemId);
            localStorage.setItem('cartItems1', JSON.stringify(state.cartItems));
        }
    },
});

export const { addToCart, removeFromCart } = cartSlice.actions;

export default cartSlice.reducer;