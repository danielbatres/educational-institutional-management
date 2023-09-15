using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase {
  private readonly IChatService _chatService;
  
  public ChatController(IChatService chatService) {
    _chatService = chatService;
  }
  
  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Post(ChatMessage chat) {
    await _chatService.Create(chat);
    
    return Ok();
  }
}