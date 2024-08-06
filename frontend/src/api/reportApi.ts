import axios from 'axios';
import dayjs, { Dayjs } from 'dayjs';

const API_BASE_URL = 'https://localhost:7005/api/Report';

export const fetchReportData = async (startDate: Dayjs, endDate: Dayjs) => {
  const startDateISO = startDate.toISOString();
  const endDateISO = endDate.toISOString();

  try {
    const response = await axios.get(`${API_BASE_URL}/monthly-report`, {
      params: {
        startDate: startDateISO,
        endDate: endDateISO,
      },
    });
    return response.data;
  } catch (error) {
    console.error('Failed to fetch data', error);
    throw error;
  }
};
export const fetchEmployeeContractReport = async (startDate: Dayjs, endDate: Dayjs) => {
    const startDateISO = startDate.toISOString();
    const endDateISO = endDate.toISOString();
  
    try {
      const response = await axios.get(`${API_BASE_URL}/employee-contract-report`, {
        params: {
          startDate: startDateISO,
          endDate: endDateISO,
        },
      });
      return response.data;
    } catch (error) {
      console.error('Failed to fetch data', error);
      throw error;
    }
  };

  export const fetchAllAssets = async () => {
    try {
      const response = await axios.get(`${API_BASE_URL}/asset-report`);
      return response.data;
    } catch (error) {
      console.error('Lỗi khi fetch data từ API:', error);
      throw error;
    }
  };
  