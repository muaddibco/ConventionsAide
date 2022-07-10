using AutoMapper;
using ConventionsAide.Core.Communication;
using ConventionsAide.Users.Contracts;
using ConventionsAide.Users.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.Users.Manager.Handlers
{
    public class CreateUserHandler : ApiHandlerBase<CreateUserRequestDto, UserDto>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public override async Task<UserDto> HandleAsync(CommandMessage<CreateUserRequestDto> message, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.InsertAsync(_mapper.Map<CreateUserRequestDto, User>(message.Payload), true, cancellationToken);

            return _mapper.Map<User, UserDto>(user);
        }
    }
}
