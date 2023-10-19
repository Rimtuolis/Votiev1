﻿using VotieAPI.Data.Entities;
using VotieAPI.Requests;
using VotieAPI.Responses;

namespace VotieAPI.Mappers
{
    public static class VoteMapper
    {
        public static CreatedVoteResponse MapToApiResponse(this Vote vote)
        {
            return new CreatedVoteResponse()
            {
                Candidate = vote.Candidate.Name,
                Voter = vote.Voter.Name,
                Date = vote.Date.Value,
                Id = vote.Id
            };
        }
        public static Vote MapToDbEntity(this CreateVoteRequest input)
        {
            return new Vote()
            {
                Date = DateTime.Now
            };
        }
        public static Vote UpdateMapToDbEntity(this UpdateCandidateRequest input)
        {
            return new Vote()
            {
            };
        }
    }
}
