import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'https://localhost:7005/api/Account',
  headers: {
    'Content-Type': 'application/json',
  },
});

export const getAccounts = async () => {
  try {
    const response = await apiClient.get('/account');
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const getAccountById = async (accountId: number) => {
  try {
    const response = await apiClient.get(`/account/${accountId}`);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const getAccountsByActive = async (active: boolean) => {
  try {
    const response = await apiClient.get(`/account/active/${active}`);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const createAccount = async (account: any) => {
  try {
    const response = await apiClient.post('/account', account);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const updateAccount = async (accountId: number, account: any) => {
  try {
    const response = await apiClient.put(`/account/${accountId}`, account);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const deleteAccount = async (accountId: number) => {
  try {
    const response = await apiClient.delete(`/account/${accountId}`);
    return response.data;
  } catch (error) {
    throw error;
  }
};
