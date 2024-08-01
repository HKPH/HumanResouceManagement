import axios from 'axios';

export const fetchBirthdayEmployees = async () => {
  try {
    const response = await axios.get(`https://localhost:7005/api/Employee/DOB?days=2`);
    return response.data;
  } catch (error) {
    console.error('Error fetching birthday employees:', error);
    return [];
  }
};