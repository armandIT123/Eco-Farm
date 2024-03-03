import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import './index.css'
import { BrowserRouter } from 'react-router-dom'
import { configureStore } from '@reduxjs/toolkit'
import { Provider } from 'react-redux'

import suppliersReducer from './Features/suppliersSlice.js'
import { suppliersApi } from './Features/suppliersApi.js';

import cartReducer from './Features/cartSlice.js'

import authReducer from './Features/authSlice.js'
import { authApi } from './Features/authApi.js'

const store = configureStore({
  reducer: {
    cart: cartReducer,
    [suppliersApi.reducerPath]: suppliersApi.reducer,
    [authApi.reducerPath]: authApi.reducer,
    auth: authReducer,

  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(authApi.middleware).concat(suppliersApi.middleware)

});

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <BrowserRouter>
      <Provider store={store}>
        <App />
      </Provider>
    </BrowserRouter>
  </React.StrictMode>,
)
