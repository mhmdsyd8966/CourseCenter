namespace CourseCenter.Helpers
{
	public  class UploadPhoto
	{
		public static string UploadImage(string ControllerName,IFormFile photo)
		{
			if (photo != null)
			{
				string fileExtention = Path.GetExtension(photo.FileName);
				var filepath = Path.Combine($"D:\\CourseCenter\\CourseCenter\\wwwroot\\{ControllerName}\\", photo.FileName);
				using (var stream = System.IO.File.Create(filepath))
				{

					photo.CopyTo(stream);
				}
				return $"{ControllerName+"/"+photo.FileName}";
			}
			throw new ArgumentNullException(nameof(photo));
		}
	}
}
