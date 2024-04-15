using CloudinaryDotNet.Actions;

namespace JobFinder.Interfaces
{
	public interface IPhotoService
	{
		Task<ImageUploadResult> AppPhotoAsync(IFormFile file);
		Task<DeletionResult> DeletePhotoAsync(string publiId); 
	}
}
