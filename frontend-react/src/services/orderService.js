export const getOrders = async (api) => {
    const response = await api.get('/Ordem');
    return response.data;
};

export const getOrderById = async (api, id) => {
    const response = await api.get(`/Ordem/${id}`);
    return response.data;
};

export const createOrder = async (api, data) => {
    const response = await api.post('/Ordem', data);
    return response.data;
};

export const updateOrder = async (api, id, data) => {
    const response = await api.put(`/Ordem/${id}`, data);
    return response.data;
};

export const deleteOrder = async (api, id) => {
    const response = await api.delete(`/Ordem/${id}`);
    return response.data;
};
