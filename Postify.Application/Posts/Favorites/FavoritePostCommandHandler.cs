using ErrorOr;

using MediatR;

using Postify.Application.Common.Interfaces;
using Postify.Domain.Entities;
using Postify.Domain.Errors;

namespace Postify.Application.Posts.Favorites
{
    public class FavoritePostCommandHandler : IRequestHandler<FavoritePostCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FavoritePostCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(FavoritePostCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(command.Email);

            if(user is null) return Errors.User.NotFound;

            var currentFavorite = await _unitOfWork.FavoriteRepository.CheckFavoriteExistsAsync(user.Id, command.PostId);

            // Delete current favorite
            if(currentFavorite is not null)
            {
                _unitOfWork.FavoriteRepository.Delete(currentFavorite);
            }
            // Add new favorite
            else
            {
                var newFavorite = new Favorite(user.Id, command.PostId);
                await _unitOfWork.FavoriteRepository.AddAsync(newFavorite);
            }

            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}