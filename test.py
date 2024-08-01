from datetime import datetime

def is_date_in_range_ignoring_year(a, b, c):
    # Chuyển đổi tất cả các giá trị datetime về cùng năm (ví dụ năm 1) để dễ dàng so sánh
    def to_date_without_year(date):
        return datetime(1, date.month, date.day)
    
    a_ignore_year = to_date_without_year(a)
    b_ignore_year = to_date_without_year(b)
    c_ignore_year = to_date_without_year(c)
    
    # Đảm bảo a nhỏ hơn hoặc bằng c
    if a_ignore_year > c_ignore_year:
        # Hoán đổi giá trị a và c nếu a lớn hơn c
        a_ignore_year, c_ignore_year = c_ignore_year, a_ignore_year

    # Kiểm tra xem b có nằm trong khoảng từ a đến c hay không
    return a_ignore_year <= b_ignore_year <= c_ignore_year

# Ví dụ sử dụng
a = datetime(2023, 3, 3)
b = datetime(2024, 4, 3)
c = datetime(2024, 4, 4)

result = is_date_in_range_ignoring_year(a, b, c)
print(f"Date {b.strftime('%m/%d')} is in range from {a.strftime('%m/%d')} to {c.strftime('%m/%d')}: {result}")
