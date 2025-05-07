using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects.Avatar;
using Shared.DataTransferObjects.BodyPart;
using Shared.DataTransferObjects.Category;
using Shared.DataTransferObjects.Exercise;
using Shared.DataTransferObjects.User;

namespace PulseWorkout;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User
        CreateMap<UserForRegistrationDto, User>();

        // Exercise
        CreateMap<Exercise, FullExerciseDto>();
        CreateMap<Exercise, BasicExerciseDto>();
        CreateMap<ExerciseForCreationDto, Exercise>();
        CreateMap<ExerciseForUpdateDto, Exercise>();
            // User Exercises
        CreateMap<UserExerciseForCreationDto, Exercise>();
        CreateMap<UserExerciseForUpdateDto, Exercise>();

        // BodyPart
        CreateMap<BodyPart, BodyPartDto>();
        CreateMap<BodyPartForCreationDto, BodyPart>();
        CreateMap<BodyPartForUpdateDto, BodyPart>();

        // Category
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryForCreationDto, Category>();
        CreateMap<CategoryForUpdateDto, Category>();

        // Avatar
        CreateMap<Avatar, AvatarDto>();
        CreateMap<AvatarForCreationDto, Avatar>();
        CreateMap<AvatarForUpdateDto, Avatar>();
    }
}