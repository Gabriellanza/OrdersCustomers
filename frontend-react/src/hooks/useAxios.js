import { useEffect } from 'react';
import axios from 'axios';
import toast from 'react-hot-toast';
import { useAuth0 } from '@auth0/auth0-react';

const api = axios.create({
    baseURL: import.meta.env.VITE_API_URL,
});

export const useAxios = () => {
    const { getAccessTokenSilently } = useAuth0();

    useEffect(() => {
        const requestInterceptor = api.interceptors.request.use(
            async (config) => {
                try {
                    const token = await getAccessTokenSilently();
                    config.headers.Authorization = `Bearer ${token}`;
                } catch (error) {
                    console.error("Error ao obter Token", error);
                }
                return config;
            },
            (error) => Promise.reject(error)
        );

        const responseInterceptor = api.interceptors.response.use(
            (response) => response,
            (error) => {
                if (error.response && error.response.status === 400) {
                    const errors = error.response.data.errors;
                    if (errors && Array.isArray(errors)) {
                        errors.forEach(err => {
                            toast.error(err.value);
                        });
                    }
                }
                return Promise.reject(error);
            }
        );

        return () => {
            api.interceptors.request.eject(requestInterceptor);
            api.interceptors.response.eject(responseInterceptor);
        };
    }, [getAccessTokenSilently]);

    return api;
};

export default api;
