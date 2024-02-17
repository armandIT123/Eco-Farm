import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";

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

export default suppliersSlice.reducer;