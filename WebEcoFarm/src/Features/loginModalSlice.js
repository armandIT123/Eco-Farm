import { createSlice } from '@reduxjs/toolkit';

const initialState = {
  showModal: false,
};

export const loginModalSlice = createSlice({
  name: 'loginModal',
  initialState,
  reducers: {
    openModal: state => {
      state.showModal = true;
    },
    closeModal: state => {
      state.showModal = false;
    },
  },
});

export const { openModal, closeModal } = loginModalSlice.actions;

export default loginModalSlice.reducer;
