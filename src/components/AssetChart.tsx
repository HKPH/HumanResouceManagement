import React, { useState, useEffect } from 'react';
import { Pagination } from 'antd';
import { Column } from '@ant-design/charts';
import { fetchAllAssets } from '../api/reportApi';

const AssetsChart: React.FC = () => {
  const [allAssets, setAllAssets] = useState<any[]>([]);
  const [assets, setAssets] = useState<any[]>([]);
  const [totalAssets, setTotalAssets] = useState<number>(0);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const pageSize = 10;

  useEffect(() => {
    const fetchAPIAssets = async () => {
      try {
        const data = await fetchAllAssets();
        setAllAssets(data);
        setTotalAssets(data.length);
      } catch (error) {
        console.error('Lỗi khi fetch data từ API:', error);
      }
    };

    fetchAPIAssets();
  }, []);

  useEffect(() => {
    const start = (currentPage - 1) * pageSize;
    const end = start + pageSize;
    setAssets(allAssets.slice(start, end));
  }, [allAssets, currentPage]);

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
    color: ['#1f77b4', '#ff7f0e'],
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
