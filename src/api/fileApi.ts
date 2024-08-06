import axios from 'axios';

const API_URL = "https://localhost:7005/api/Attachments";

export const fetchFiles = async () => {
  try {
    const response = await axios.get(API_URL);
    return response.data;
  } catch (error) {
    console.error('There was an error fetching the files!', error);
    throw error;
  }
};

export const uploadFile = async (file: File, userId: string) => {
  const formData = new FormData();
  formData.append('file', file);
  formData.append('employeeId', userId);

  try {
    const response = await axios.post(`${API_URL}/upload`, formData);
    return response.data;
  } catch (error) {
    console.error('There was an error uploading the file!', error);
    throw error;
  }
};

export const deleteFile = async (id: number) => {
  try {
    await axios.delete(`${API_URL}/${id}`);
  } catch (error) {
    console.error('There was an error deleting the file!', error);
    throw error;
  }
};
export const downloadFile = async (id: number) => {
    try {
      const response = await axios.get(`${API_URL}/download/${id}`, {
        responseType: 'blob', 
      });
      return response.data;
    } catch (error) {
      console.error('There was an error downloading the file!', error);
      throw error;
    }
  };