import axios from 'axios';

// URL của API
const API_URL = 'https://localhost:7005/api/Bank';

// Lấy danh sách ngân hàng (toàn bộ hoặc chỉ lấy các ngân hàng đang hoạt động)
export const getBanks = async (active = false) => {
  try {
    const url = active ? `${API_URL}/active/true` : API_URL;
    const response = await axios.get(url);
    return response.data; // Trả về dữ liệu từ API
  } catch (error) {
    console.error('Error fetching banks:', error);
    throw error; // Ném lỗi lên để xử lý ở nơi gọi hàm
  }
};

// Tạo ngân hàng mới
export const createBank = async (bank:any) => {
  try {
    const response = await axios.post(API_URL, bank);
    return response.data; // Trả về dữ liệu từ API
  } catch (error) {
    console.error('Error creating bank:', error);
    throw error; // Ném lỗi lên để xử lý ở nơi gọi hàm
  }
};

// Cập nhật ngân hàng
export const updateBank = async (id:number, bank:any) => {
  try {
    const response = await axios.put(`${API_URL}/${id}`, bank);
    return response.data; // Trả về dữ liệu từ API
  } catch (error) {
    console.error('Error updating bank:', error);
    throw error; // Ném lỗi lên để xử lý ở nơi gọi hàm
  }
};

// Xóa ngân hàng
export const deleteBank = async (id:number) => {
  try {
    console.error('Id ngân hàng xóa:', id);
    const response = await axios.delete(`${API_URL}/${id}`);
    return response.data; // Trả về dữ liệu từ API
  } catch (error) {
    console.error('Error deleting bank:', error);
    throw error; // Ném lỗi lên để xử lý ở nơi gọi hàm
  }
};
