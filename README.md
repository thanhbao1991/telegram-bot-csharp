# Telegram Bot C# - Render Ready

## Mô tả
Bot Telegram đơn giản viết bằng C#, trả lời "Hello 👋" cho mọi tin nhắn. Triển khai được ngay lên Render bằng Docker.

## Biến môi trường
- TELEGRAM_TOKEN = Token của bot

## Cách chạy local
```bash
dotnet run
```

## Deploy trên Render
1. Tạo dịch vụ mới, chọn Environment là Docker
2. Thêm biến môi trường TELEGRAM_TOKEN
3. Deploy!