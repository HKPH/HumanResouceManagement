import axios from 'axios';

const API_URL = 'https://localhost:7005/api/Auth';
export interface AccountDto {
    id: number;
    username: string;
    password: string;
    role: string;
    email: string;
    employeeId: number;
    active: boolean;
}

export const login = async (credentials: AccountDto) => {
    try {
        const response = await axios.post(`${API_URL}/login`, credentials);
        return response.data;
    } catch (error) {
        throw error;
    }
};
