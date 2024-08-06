import axios from 'axios';

const API_URL = 'https://localhost:7005/api/Bank';

export const getBanks = async (active = false) => {
  try {
    const url = active ? `${API_URL}/active/true` : API_URL;
    const response = await axios.get(url);
    return response.data;
  } catch (error) {
    console.error('Error fetching banks:', error);
    throw error; 
  }
};

export const createBank = async (bank:any) => {
  try {
    const response = await axios.post(API_URL, bank);
    return response.data; 
  } catch (error) {
    console.error('Error creating bank:', error);
    throw error;
  }
};

export const updateBank = async (id:number, bank:any) => {
  try {
    const response = await axios.put(`${API_URL}/${id}`, bank);
    return response.data;
  } catch (error) {
    console.error('Error updating bank:', error);
    throw error; 
  }
};

export const deleteBank = async (id:number) => {
  try {
    console.error('Id ngân hàng xóa:', id);
    const response = await axios.delete(`${API_URL}/${id}`);
    return response.data;
  } catch (error) {
    console.error('Error deleting bank:', error);
    throw error; 
  }
};
