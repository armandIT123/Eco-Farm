import { createSlice } from "@reduxjs/toolkit";
import { orderApi } from "./orderApi";


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
                state.cartItems[itemIdex].quantity += action.payload.quantity;            
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
            if(itemIdex >= 0){
                state.cartItems[itemIdex].quantity = action.payload.quantity;
                localStorage.setItem('cartItems1', JSON.stringify(state.cartItems));
            }
        },
        removeFromCart(state, action) {
            const itemId = action.payload;
            state.cartItems = state.cartItems.filter(item => item.id !== itemId);
            localStorage.setItem('cartItems1', JSON.stringify(state.cartItems));
        }
    },
    extraReducers: (builder) => {
        builder.addMatcher(
            orderApi.endpoints.placeOrder.matchFulfilled,
            (state, action) => {
                state.cartItems = null;
                localStorage.removeItem('cartItems1');
            }
        )
    },
});

export const { addToCart, changeQuantity, removeFromCart } = cartSlice.actions;

export default cartSlice.reducer;