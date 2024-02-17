import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    cartItems: [],
    cartTotalQuantity: 0,
    cartTotalAmount: 0,
}

const cartSlice = createSlice({
    name: "cart",
    initialState,
    reducers: {
        addToCart(state, action){
            const itemIdex = state.cartItems.findIndex((item)=>item.id === action.payload.id);

            if(itemIdex >= 0)
            {
                state.cartItems[itemIdex].cartQuantity += 1;
            }
            else
            {
                const tempProduct = { ...action.payload, cartQuantity: 1};
                state.cartItems.push(tempProduct);
            }
        },
    },
});

export const { addToCart } = cartSlice.actions;

export default cartSlice.reducer;