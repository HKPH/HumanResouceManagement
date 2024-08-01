import React, { useState, useEffect } from 'react';
import { Pagination } from 'antd';
import { Column } from '@ant-design/charts';

// Mock data (Replace this with the actual fetch from your API in production)
const mockData = [
  { id: 1, name: 'Máy tính xách tay', totalQuantity: 10, borrowedQuantity: 5 },
  { id: 2, name: 'Bàn ghế văn phòng', totalQuantity: 50, borrowedQuantity: 40 },
  { id: 3, name: 'Máy chiếu', totalQuantity: 30, borrowedQuantity: 20 },
  { id: 4, name: 'Điện thoại', totalQuantity: 15, borrowedQuantity: 7 },
  { id: 5, name: 'Máy in', totalQuantity: 8, borrowedQuantity: 3 },
  { id: 6, name: 'Bàn làm việc', totalQuantity: 20, borrowedQuantity: 12 },
  { id: 7, name: 'Ghế văn phòng', totalQuantity: 25, borrowedQuantity: 15 },
  { id: 8, name: 'Tủ tài liệu', totalQuantity: 10, borrowedQuantity: 6 },
  { id: 9, name: 'Máy lạnh', totalQuantity: 7, borrowedQuantity: 4 },
  { id: 10, name: 'Máy chiếu mini', totalQuantity: 12, borrowedQuantity: 8 },
  { id: 11, name: 'Laptop', totalQuantity: 5, borrowedQuantity: 2 },
  { id: 12, name: 'Bàn họp', totalQuantity: 3, borrowedQuantity: 1 },
  { id: 13, name: 'Ghế xoay', totalQuantity: 18, borrowedQuantity: 10 },
  { id: 14, name: 'Tủ đựng hồ sơ', totalQuantity: 6, borrowedQuantity: 3 },
  { id: 15, name: 'Máy tính để bàn', totalQuantity: 11, borrowedQuantity: 6 },
  { id: 16, name: 'Bàn làm việc nhỏ', totalQuantity: 8, borrowedQuantity: 5 },
  { id: 17, name: 'Bàn phím', totalQuantity: 30, borrowedQuantity: 18 },
  { id: 18, name: 'Chuột máy tính', totalQuantity: 40, borrowedQuantity: 22 },
  { id: 19, name: 'Điện thoại bàn', totalQuantity: 12, borrowedQuantity: 6 },
  { id: 20, name: 'Máy tính bảng', totalQuantity: 7, borrowedQuantity: 3 },
];

const AssetsChart: React.FC = () => {
  const [assets, setAssets] = useState<any[]>([]);
  const [totalAssets, setTotalAssets] = useState<number>(0);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const pageSize = 10;

  useEffect(() => {
    const fetchAssets = () => {
      const start = (currentPage - 1) * pageSize;
      const end = start + pageSize;
      const pagedData = mockData.slice(start, end);

      setAssets(pagedData);
      setTotalAssets(mockData.length);
    };

    fetchAssets();
  }, [currentPage]);

  const handlePageChange = (page: number) => {
    setCurrentPage(page);
  };

  const data = assets.flatMap(asset => [
    { name: asset.name, type: 'Borrowed', value: asset.borrowedQuantity },
    { name: asset.name, type: 'Total', value: asset.totalQuantity },
  ]);

  const config = {
    data,
    isGroup: true,
    xField: 'name',
    yField: 'value',
    seriesField: 'type',
    color: ['#1f77b4', '#ff7f0e'], // Different colors for each type
    label: {
      position: 'top',
    },
  };

  return (
    <div>
      <Column {...config} />
      <Pagination
        current={currentPage}
        pageSize={pageSize}
        total={totalAssets}
        onChange={handlePageChange}
        style={{ marginTop: 16 }}
      />
    </div>
  );
};

export default AssetsChart;
