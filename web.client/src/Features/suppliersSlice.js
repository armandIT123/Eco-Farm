import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    items: [],
    status: null,
    error: null,
}

const suppliersSlice = createSlice({
    name: "suppliers",
    initialState,
    reducers: {},
});

export const selectSupplierById = (state, supplierId) => 
  state.suppliers.items.find(supplier => supplier.id === supplierId);
  
export default suppliersSlice.reducer;