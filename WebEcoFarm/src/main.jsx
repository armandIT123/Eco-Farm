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

const store = configureStore({
  reducer: {
    cart: cartReducer,
    [suppliersApi.reducerPath]: suppliersApi.reducer,

  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(suppliersApi.middleware)

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
