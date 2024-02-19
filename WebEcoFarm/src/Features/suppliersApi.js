import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

export const suppliersApi = createApi({
    reducerPath: "suppliersApi",
    baseQuery: fetchBaseQuery({ baseUrl: "https://localhost:7184" }),
    endpoints: (builder) => ({
        getAllSuppliers: builder.query({
            query: () => "supplier",
        }),
        getSupplierById: builder.query({
            query: (id) => "GetSupplier?supplierId=" + id,
        }),
    }),
});



export const { useGetAllSuppliersQuery } = suppliersApi;
