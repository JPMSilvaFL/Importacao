using Flunt.Notifications;

namespace Importacao.Application.DTOs;

public class ResultDto<T> where T : class {
	public static ResultDto<T> Ok(T data) {
		return new ResultDto<T>(true, data);
	}

	public static ResultDto<T> Fail(IReadOnlyCollection<Notification> notifications) {
		return new ResultDto<T>(false, notifications);
	}

	public bool Success { get; private set; }
	public IList<T> Data { get; private set; } = [];
	public IReadOnlyCollection<Notification> Errors { get; private set; } = [];

	public ResultDto(bool success, IList<T> data) {
		Success = success;
		Data = data;
	}

	public ResultDto(bool success, T data) {
		Success = success;
		Data.Add(data);
	}

	public ResultDto(bool success, IReadOnlyCollection<Notification> errors) {
		Success = success;
		Errors = errors;
	}
}