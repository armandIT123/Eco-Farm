import { createSlice } from "@reduxjs/toolkit";
import { authApi } from "./authApi";

const initialState = {
    user: sessionStorage.getItem('loggedUser') !== null ? JSON.parse(sessionStorage.getItem('loggedUser')) : null,
}

const authSlice = createSlice({
    name: 'auth',
    initialState,
    extraReducers: (builder) => {
        builder.addMatcher(
            authApi.endpoints.checkToken.matchFulfilled,
            (state, action) => {
                state.user = action.payload;
                sessionStorage.setItem('loggedUser', JSON.stringify(state.user));
            }
        );
        builder.addMatcher(
            authApi.endpoints.login.matchFulfilled,
            (state, action) => {
                state.user = action.payload;
                sessionStorage.setItem('loggedUser', JSON.stringify(state.user));
            }
        );
        builder.addMatcher(
            authApi.endpoints.logOut.matchFulfilled,
            (state, action) => {
                state.user = null;
                sessionStorage.removeItem('loggedUser');
            }
        );
    }
})

export const selectCurrentUser = (state) => state.auth.user;

export default authSlice.reducer;


