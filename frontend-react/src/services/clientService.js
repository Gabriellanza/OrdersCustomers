export const getClients = async (api) => {
    const response = await api.get('/Cliente');
    return response.data;
};

export const getClientById = async (api, id) => {
    const response = await api.get(`/Cliente/${id}`);
    return response.data;
};

export const createClient = async (api, data) => {
    const response = await api.post('/Cliente', data);
    return response.data;
};

export const updateClient = async (api, id, data) => {
    const response = await api.put(`/Cliente/${id}`, data);
    return response.data;
};

export const deleteClient = async (api, id) => {
    const response = await api.delete(`/Cliente/${id}`);
    return response.data;
};
