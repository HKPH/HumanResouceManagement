import axios from 'axios';

const API_URL = "https://localhost:7005/api/Employee";

export const createEmployee = async () => {
    try {
        const response = await axios.post(`${API_URL}`, {});
        return response.data;
    } catch (error) {
        console.error('Error creating employee:', error);
        throw error;
    }
};
