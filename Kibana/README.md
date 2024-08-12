## Giải thích về thuật ngữ trong Kibana


Metrics

Metrics (hay còn gọi là “Metric aggregations”) là các phép toán tổng hợp trên dữ liệu. Chúng giúp bạn tính toán các giá trị từ dữ liệu của bạn. Một số ví dụ về metrics bao gồm:

	•	Average: Tính giá trị trung bình của một trường.
	•	Sum: Tính tổng của một trường.
	•	Max: Tìm giá trị lớn nhất của một trường.
	•	Min: Tìm giá trị nhỏ nhất của một trường.
	•	Count: Đếm số lượng tài liệu (documents) hoặc giá trị.

Ví dụ: Nếu bạn có một trường salary trong dữ liệu và bạn muốn tính tổng tất cả các mức lương, bạn sẽ sử dụng phép toán Sum trong phần metrics.

Buckets

Buckets (hay còn gọi là “Bucket aggregations”) được dùng để phân loại và nhóm dữ liệu thành các bucket hoặc nhóm. Buckets không tính toán giá trị tổng hợp mà thay vào đó phân loại dữ liệu theo các tiêu chí khác nhau. Một số ví dụ về buckets bao gồm:

	•	Date Histogram: Nhóm dữ liệu theo các khoảng thời gian.
	•	Terms: Nhóm dữ liệu theo các giá trị của một trường cụ thể (ví dụ: nhóm theo quốc gia).
	•	Range: Phân loại dữ liệu vào các khoảng giá trị.
	•	Filters: Phân loại dữ liệu dựa trên các bộ lọc tùy chỉnh.

Ví dụ: Nếu bạn muốn phân loại dữ liệu theo các khoảng thời gian hàng tháng và tính tổng mức lương cho mỗi tháng, bạn sẽ sử dụng Date Histogram trong phần buckets để nhóm dữ liệu theo tháng, và Sum trong phần metrics để tính tổng mức lương.