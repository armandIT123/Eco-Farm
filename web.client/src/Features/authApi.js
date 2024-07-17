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

        checkToken: builder.query({
            query: () => ({
                url: '/user/check-auth-status',
                method: 'GET'
            }),
        }),

        login: builder.mutation({
            query: (credentials) => ({
                url: '/user/login',
                method: 'POST',
                credentials: "include",
                body: credentials
            })
        }),

        register: builder.mutation({
            query: (registerDto) =>({
                url: '/user/register',
                method: 'POST',
                credentials: "include",
                body:  registerDto,                
            })
        }),

        checkEmail: builder.mutation({
            query: (email) => ({
                url: '/user/checkEmail',
                method: 'POST',                
                headers: {
                    'Content-Type': 'application/json', 
                },
                credentials: 'include',
                body:  email ,
            })
        }),

        logOut: builder.mutation({
            query: () => ({
                url: '/user/logout',
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json', 
                },
                credentials: "include",
                body:  '' ,               
            })
        }),

    })
});

export const { 
    useCheckTokenQuery, 
    useLoginMutation, 
    useRegisterMutation, 
    useCheckEmailMutation, 
    useLogOutMutation } = authApi;



