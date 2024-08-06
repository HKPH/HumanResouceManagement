import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'https://localhost:7005/api/Account',
  headers: {
    'Content-Type': 'application/json',
  },
});

export const getAccounts = async () => {
  try {
    const response = await apiClient.get('');
    return response.data;
  } catch (error) {
    console.error('Error fetching accounts:', error);
    throw error;
  }
};

export const getAccountById = async (accountId: number) => {
  try {
    const response = await apiClient.get(`/${accountId}`);
    return response.data;
  } catch (error) {
    console.error('Error fetching account by ID:', error);
    throw error;
  }
};

export const getAccountsByActive = async (active: boolean) => {
  try {
    const response = await apiClient.get(`/active/${active}`);
    return response.data;
  } catch (error) {
    console.error('Error fetching accounts by active status:', error);
    throw error;
  }
};

export const createAccount = async (account: any) => {
  try {
    const response = await apiClient.post('', account);
    return response.data;
  } catch (error) {
    console.error('Error creating account:', error);
    throw error;
  }
};

export const updateAccount = async (accountId: number, account: any) => {
  try {
    const response = await apiClient.put(`/${accountId}`, account);
    return response.data;
  } catch (error) {
    console.error('Error updating account:', error);
    throw error;
  }
};

export const deleteAccount = async (accountId: number) => {
  try {
    const response = await apiClient.delete(`/${accountId}`);
    return response.data;
  } catch (error) {
    console.error('Error deleting account:', error);
    throw error;
  }
};
