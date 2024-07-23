import axios from 'axios';

const BASE_URL = 'https://localhost:7005/api/Department';

export const getDepartments = async () => {
  const response = await axios.get(`${BASE_URL}`);
  return response.data.$values;
};

export const getActiveDepartments = async () => {
  const response = await axios.get(`${BASE_URL}/active/true`);
  return response.data.$values;
};

// Add other API calls like POST, PUT, DELETE
export const createDepartment = async (department: Department) => {
  const response = await axios.post(`${BASE_URL}`, department);
  return response.data;
};

export const updateDepartment = async (department: Department) => {
  const response = await axios.put(`${BASE_URL}`, department);
  return response.data;
};

export const deleteDepartment = async (departmentId: number) => {
  const response = await axios.delete(`${BASE_URL}/${departmentId}`);
  return response.data;
};

export interface Department {
  id: number;
  name: string;
  active: boolean;
  parentDepartmentId: number | null;
}
