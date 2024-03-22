import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

export const authApi = createApi({
    reducerPath: "orderApi",
    baseQuery: fetchBaseQuery({ baseUrl: "https://localhost:7184" }),
    endpoints: builder => ({
        
        placeOrder: builder.mutation({
            query: (order) => ({
                url: '/',
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