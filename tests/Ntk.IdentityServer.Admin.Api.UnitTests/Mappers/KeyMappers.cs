using System;
using FluentAssertions;
using Ntk.IdentityServer.Admin.Api.Dtos.Key;
using Ntk.IdentityServer.Admin.Api.Mappers;
using Ntk.IdentityServer.Admin.Api.UnitTests.Mocks;
using Ntk.IdentityServer.Admin.BusinessLogic.Dtos.Key;
using Ntk.IdentityServer.Admin.UnitTests.Mocks;
using Xunit;

namespace Ntk.IdentityServer.Admin.Api.UnitTests.Mappers
{
    public class KeyMappers
    {
        [Fact]
        public void CanMapKeyDtoToKayApiDto()
        {
            var keyDto = KeyDtoMock.GenerateRandomKey(Guid.NewGuid().ToString());

            var keyApi = keyDto.ToKeyApiModel<KeyApiDto>();

            keyApi.Should().BeEquivalentTo(keyDto);
        }

        [Fact]
        public void CanMapKeyApiDtoToKeyDto()
        {
            var keyApiDto = KeyApiDtoMock.GenerateRandomKey(Guid.NewGuid().ToString());

            var keyDto = keyApiDto.ToKeyApiModel<KeyDto>();

            keyDto.Should().BeEquivalentTo(keyApiDto);
        }
    }
}