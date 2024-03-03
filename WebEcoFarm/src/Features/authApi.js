import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

  /*  prepareHeaders: (headers, {getState}) => {
        const token = getState().auth.token;
        if(token){
            headers.set("authorization", `Bearer ${token}`);
        }
        return headers;
    }*/

export const authApi = createApi({
    reducerPath: "authApi",
    baseQuery: fetchBaseQuery({ baseUrl: "https://localhost:7184" }),
    endpoints: builder => ({

        login: builder.mutation({
            query: credentials => ({
                url: '/user',
                method: 'POST',
                body: {...credentials}
            })
        }),

        checkEmail: builder.mutation({
            query: (email) => ({
                url: '/user/checkEmail',
                method: 'POST',
                body: email,
            })
        }),

    })
});

export const { useLoginMutation, useCheckEmailMutation } = authApi;



