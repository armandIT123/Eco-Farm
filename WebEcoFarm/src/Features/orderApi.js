import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

export const orderApi = createApi({
    reducerPath: "orderApi",
    baseQuery: fetchBaseQuery({ baseUrl: "https://localhost:7184" }),
    endpoints: builder => ({
        
        placeOrder: builder.mutation({
            query: (order) => ({
                url: '/order/place-order',
                method: 'POST',
                credentials: "include",
                body: order
            })
        }),
        getOrders: builder.query({
            query: () => ({
                url: '/',
                method: 'GET',
                credentials: "include",
            }),
        }),
    })
})

export const { 
    usePlaceOrderMutation,
    useGetOrdersQuery } = orderApi;