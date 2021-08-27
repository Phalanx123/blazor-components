using Majorsoft.Blazor.Components.CssEvents.Transition;

using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using Majorsoft.Blazor.Components.Timer;
using System;
using Majorsoft.Blazor.Components.CommonTestsBase;

namespace Majorsoft.Blazor.Components.Notifications.Tests.Toasts
{
	[TestClass]
	public class ToastContainerTest : ComponentsTestBase<ToastContainer>
	{
		private Mock<ITransitionEventsService> _transitionMock;
		private Mock<IToastService> _toastServiceMock;
		private Mock<IToastInternals> _toastInternalsMock;

		[TestInitialize]
		public void Init()
		{
			var logger = new Mock<ILogger<AdvancedTimer>>();
			var logger2 = new Mock<ILogger<Toast>>();
			_transitionMock = new Mock<ITransitionEventsService>();
			_toastServiceMock = new Mock<IToastService>();
			_toastInternalsMock = new Mock<IToastInternals>();

			_testContext.Services.Add(new ServiceDescriptor(typeof(ILogger<AdvancedTimer>), logger.Object));
			_testContext.Services.Add(new ServiceDescriptor(typeof(ILogger<Toast>), logger2.Object));
			_testContext.Services.Add(new ServiceDescriptor(typeof(ITransitionEventsService), _transitionMock.Object));
			_testContext.Services.Add(new ServiceDescriptor(typeof(IToastService), _toastServiceMock.Object));
			_testContext.Services.Add(new ServiceDescriptor(typeof(IToastInternals), _toastInternalsMock.Object));
			_testContext.Services.Add(new ServiceDescriptor(typeof(SingletonComponentService<ToastContainer>), new SingletonComponentService<ToastContainer>()));
		}

		[TestMethod]
		public void ToastContainer_should_not_rendered_anything_until_has_Toasts()
		{
			var rendered = _testContext.RenderComponent<ToastContainer>(
				("id", "id1"), //HTML attributes
				("title", "text") //HTML attributes
				);

			rendered.MarkupMatches("");
		}

		[TestMethod]
		public void ToastContainer_should_not_rendered_html_when_has_no_Toasts()
		{
			_toastInternalsMock.SetupGet(g => g.AllToasts).Returns(new ToastSettings[] { new ToastSettings() });
			_toastServiceMock.SetupGet(g => g.GlobalSettings).Returns(new  ToastContainerGlobalSettings());

			var rendered = _testContext.RenderComponent<ToastContainer>(
				("id", "id1"), //HTML attributes
				("title", "text") //HTML attributes
				);

			var div = rendered.Find("div");
			Assert.IsNotNull(div);

			rendered.WaitForAssertion(() => rendered.MarkupMatches(@"<div style=""max-width: 400px; width: 400px;"" class=""btoast-container position-topright"" id=""id1"" title=""text"" >
			  <div class=""btoast-main bnotify-normal-primary"" style=""opacity: 1; box-shadow: 1px 5px 20px 0px #c7c7c7;"" tabindex=""1000""  >
				<div class=""btoast-body"" >
				  <div >
					<svg class=""btoast-img"" focusable=""false"" viewBox=""0 0 24 24"" aria-hidden=""true"" >
					  <path fill-rule=""evenodd"" clip-rule=""evenodd"" d=""m18.598 2.865c0-0.552-0.447-1-1-1s-1 0.448-1 1v2c0 0.552 0.447 1 1 1s1-0.448 1-1v-2zm-12 2c0 0.552-0.447 1-1 1s-1-0.448-1-1v-2c0-0.552 0.447-1 1-1s1 0.448 1 1v2zm13 5v10h-16v-10h16zm2-6h-2v1c0 1.103-0.897 2-2 2s-2-0.897-2-2v-1h-8v1c0 1.103-0.897 2-2 2s-2-0.897-2-2v-1h-2v18h20v-18zm-13 10h-2v-2h2v2zm4 0h-2v-2h2v2zm4 0h-2v-2h2v2zm-8 4h-2v-2h2v2zm4 0h-2v-2h2v2zm4 0h-2v-2h2v2z"" ></path>
					</svg>
				  </div>
				  <div class=""btoast-text"" ></div>
				  <button type=""button""  class=""close normal"" >
					<span aria-hidden=""true"" >&times;</span>
					<span class=""sr-only"" >Close</span>
				  </button>
				</div>
				<div class=""btoast-progress primary start"" style=""transition: width 10s linear;"" ></div>
			  </div>
			</div>"));
		}

		[TestMethod]
		public void ToastContainer_should_not_render_Settings_Width()
		{
			_toastInternalsMock.SetupGet(g => g.AllToasts).Returns(new ToastSettings[] { new ToastSettings() });
			_toastServiceMock.SetupGet(g => g.GlobalSettings).Returns(new ToastContainerGlobalSettings() { Width = 50 });

			var rendered = _testContext.RenderComponent<ToastContainer>();

			var div = rendered.Find("div");
			Assert.IsNotNull(div);

			rendered.WaitForAssertion(() => rendered.MarkupMatches(@"<div style=""max-width: 50px; width: 50px;"" class=""btoast-container position-topright"" >
			  <div class=""btoast-main bnotify-normal-primary"" style=""opacity: 1; box-shadow: 1px 5px 20px 0px #c7c7c7;"" tabindex=""1000""  >
				<div class=""btoast-body"" >
				  <div >
					<svg class=""btoast-img"" focusable=""false"" viewBox=""0 0 24 24"" aria-hidden=""true"" >
					  <path fill-rule=""evenodd"" clip-rule=""evenodd"" d=""m18.598 2.865c0-0.552-0.447-1-1-1s-1 0.448-1 1v2c0 0.552 0.447 1 1 1s1-0.448 1-1v-2zm-12 2c0 0.552-0.447 1-1 1s-1-0.448-1-1v-2c0-0.552 0.447-1 1-1s1 0.448 1 1v2zm13 5v10h-16v-10h16zm2-6h-2v1c0 1.103-0.897 2-2 2s-2-0.897-2-2v-1h-8v1c0 1.103-0.897 2-2 2s-2-0.897-2-2v-1h-2v18h20v-18zm-13 10h-2v-2h2v2zm4 0h-2v-2h2v2zm4 0h-2v-2h2v2zm-8 4h-2v-2h2v2zm4 0h-2v-2h2v2zm4 0h-2v-2h2v2z"" ></path>
					</svg>
				  </div>
				  <div class=""btoast-text"" ></div>
				  <button type=""button""  class=""close normal"" >
					<span aria-hidden=""true"" >&times;</span>
					<span class=""sr-only"" >Close</span>
				  </button>
				</div>
				<div class=""btoast-progress primary start"" style=""transition: width 10s linear;"" ></div>
			  </div>
			</div>"));
		}

		[TestMethod]
		public void ToastContainer_should_not_render_Settings_PaddingFromSide()
		{
			var settings = new ToastContainerGlobalSettings() { PaddingFromSide = 25 };
			_toastInternalsMock.SetupGet(g => g.AllToasts).Returns(new ToastSettings[] { new ToastSettings() });
			_toastServiceMock.SetupGet(g => g.GlobalSettings).Returns(settings);

			var rendered = _testContext.RenderComponent<ToastContainer>();

			foreach (var item in Enum.GetValues<ToastPositions>())
			{
				settings.Position = item;
				_toastServiceMock.SetupGet(g => g.GlobalSettings).Returns(settings);
				rendered.Render();

				var div = rendered.Find("div");
				Assert.IsNotNull(div);

				string side = "";
				string center = "";
				if (!item.ToString().Contains("center", StringComparison.OrdinalIgnoreCase))
				{
					side = item.ToString().Contains("left", StringComparison.OrdinalIgnoreCase)
						? "left"
						: "right";
					side = $"{side}: {settings.PaddingFromSide}px;";
				}
				else
				{
					center = "margin-left: -200px;";
				}

				rendered.WaitForAssertion(() => rendered.MarkupMatches($@"<div style=""max-width: 400px; width: 400px; {side} {center}"" class=""btoast-container position-{item.ToString().ToLower()}"" >
			  <div class=""btoast-main bnotify-normal-primary"" style=""opacity: 1; box-shadow: 1px 5px 20px 0px #c7c7c7;"" tabindex=""1000""  >
				<div class=""btoast-body"" >
				  <div >
					<svg class=""btoast-img"" focusable=""false"" viewBox=""0 0 24 24"" aria-hidden=""true"" >
					  <path fill-rule=""evenodd"" clip-rule=""evenodd"" d=""m18.598 2.865c0-0.552-0.447-1-1-1s-1 0.448-1 1v2c0 0.552 0.447 1 1 1s1-0.448 1-1v-2zm-12 2c0 0.552-0.447 1-1 1s-1-0.448-1-1v-2c0-0.552 0.447-1 1-1s1 0.448 1 1v2zm13 5v10h-16v-10h16zm2-6h-2v1c0 1.103-0.897 2-2 2s-2-0.897-2-2v-1h-8v1c0 1.103-0.897 2-2 2s-2-0.897-2-2v-1h-2v18h20v-18zm-13 10h-2v-2h2v2zm4 0h-2v-2h2v2zm4 0h-2v-2h2v2zm-8 4h-2v-2h2v2zm4 0h-2v-2h2v2zm4 0h-2v-2h2v2z"" ></path>
					</svg>
				  </div>
				  <div class=""btoast-text"" ></div>
				  <button type=""button""  class=""close normal"" >
					<span aria-hidden=""true"" >&times;</span>
					<span class=""sr-only"" >Close</span>
				  </button>
				</div>
				<div class=""btoast-progress primary start"" style=""transition: width 10s linear;"" ></div>
			  </div>
			</div>"));
			}
		}

		[TestMethod]
		public void ToastContainer_should_not_render_Settings_PaddingFromTopOrBottom()
		{
			var settings = new ToastContainerGlobalSettings() { PaddingFromTopOrBottom = 22 };
			_toastInternalsMock.SetupGet(g => g.AllToasts).Returns(new ToastSettings[] { new ToastSettings() });
			_toastServiceMock.SetupGet(g => g.GlobalSettings).Returns(settings);

			var rendered = _testContext.RenderComponent<ToastContainer>();

			foreach (var item in Enum.GetValues<ToastPositions>())
			{
				settings.Position = item;
				_toastServiceMock.SetupGet(g => g.GlobalSettings).Returns(settings);
				rendered.Render();

				var div = rendered.Find("div");
				Assert.IsNotNull(div);
				
				string center = "";
				if (item.ToString().Contains("center", StringComparison.OrdinalIgnoreCase))
				{
					center = "margin-left: -200px;";
				}

				var topBottom = item.ToString().Contains("top", StringComparison.OrdinalIgnoreCase)
					? "top"
					: "bottom";
				topBottom = $"{topBottom}: {settings.PaddingFromTopOrBottom}px;";

				rendered.WaitForAssertion(() => rendered.MarkupMatches($@"<div style=""max-width: 400px; width: 400px; {topBottom} {center}"" class=""btoast-container position-{item.ToString().ToLower()}"" >
			  <div class=""btoast-main bnotify-normal-primary"" style=""opacity: 1; box-shadow: 1px 5px 20px 0px #c7c7c7;"" tabindex=""1000"" >
				<div class=""btoast-body"" >
				  <div >
					<svg class=""btoast-img"" focusable=""false"" viewBox=""0 0 24 24"" aria-hidden=""true"" >
					  <path fill-rule=""evenodd"" clip-rule=""evenodd"" d=""m18.598 2.865c0-0.552-0.447-1-1-1s-1 0.448-1 1v2c0 0.552 0.447 1 1 1s1-0.448 1-1v-2zm-12 2c0 0.552-0.447 1-1 1s-1-0.448-1-1v-2c0-0.552 0.447-1 1-1s1 0.448 1 1v2zm13 5v10h-16v-10h16zm2-6h-2v1c0 1.103-0.897 2-2 2s-2-0.897-2-2v-1h-8v1c0 1.103-0.897 2-2 2s-2-0.897-2-2v-1h-2v18h20v-18zm-13 10h-2v-2h2v2zm4 0h-2v-2h2v2zm4 0h-2v-2h2v2zm-8 4h-2v-2h2v2zm4 0h-2v-2h2v2zm4 0h-2v-2h2v2z"" ></path>
					</svg>
				  </div>
				  <div class=""btoast-text"" ></div>
				  <button type=""button""  class=""close normal"" >
					<span aria-hidden=""true"" >&times;</span>
					<span class=""sr-only"" >Close</span>
				  </button>
				</div>
				<div class=""btoast-progress primary start"" style=""transition: width 10s linear;"" ></div>
			  </div>
			</div>"));
			}
		}

		[TestMethod]
		public void ToastContainer_should_not_render_Settings_Position()
		{
			_toastInternalsMock.SetupGet(g => g.AllToasts).Returns(new ToastSettings[] { new ToastSettings() });
			_toastServiceMock.SetupGet(g => g.GlobalSettings).Returns(new ToastContainerGlobalSettings());

			var rendered = _testContext.RenderComponent<ToastContainer>();

			foreach (var item in Enum.GetValues<ToastPositions>())
			{
				_toastServiceMock.SetupGet(g => g.GlobalSettings).Returns(new ToastContainerGlobalSettings() { Position = item });
				rendered.Render();

				var div = rendered.Find("div");
				Assert.IsNotNull(div);

				string center = "";
				if (item.ToString().Contains("center", StringComparison.OrdinalIgnoreCase))
				{
					center = "margin-left: -200px;";
				}

				rendered.WaitForAssertion(() => rendered.MarkupMatches($@"<div style=""max-width: 400px; width: 400px; {center}"" class=""btoast-container position-{item.ToString().ToLower()}"" >
			  <div class=""btoast-main bnotify-normal-primary"" style=""opacity: 1; box-shadow: 1px 5px 20px 0px #c7c7c7;"" tabindex=""1000""  >
				<div class=""btoast-body"" >
				  <div >
					<svg class=""btoast-img"" focusable=""false"" viewBox=""0 0 24 24"" aria-hidden=""true"" >
					  <path fill-rule=""evenodd"" clip-rule=""evenodd"" d=""m18.598 2.865c0-0.552-0.447-1-1-1s-1 0.448-1 1v2c0 0.552 0.447 1 1 1s1-0.448 1-1v-2zm-12 2c0 0.552-0.447 1-1 1s-1-0.448-1-1v-2c0-0.552 0.447-1 1-1s1 0.448 1 1v2zm13 5v10h-16v-10h16zm2-6h-2v1c0 1.103-0.897 2-2 2s-2-0.897-2-2v-1h-8v1c0 1.103-0.897 2-2 2s-2-0.897-2-2v-1h-2v18h20v-18zm-13 10h-2v-2h2v2zm4 0h-2v-2h2v2zm4 0h-2v-2h2v2zm-8 4h-2v-2h2v2zm4 0h-2v-2h2v2zm4 0h-2v-2h2v2z"" ></path>
					</svg>
				  </div>
				  <div class=""btoast-text"" ></div>
				  <button type=""button""  class=""close normal"" >
					<span aria-hidden=""true"" >&times;</span>
					<span class=""sr-only"" >Close</span>
				  </button>
				</div>
				<div class=""btoast-progress primary start"" style=""transition: width 10s linear;"" ></div>
			  </div>
			</div>"));
			}
		}

		[TestMethod]
		public void ToastContainer_should_not_render_non_Visible_Toasts()
		{
			_toastInternalsMock.SetupGet(g => g.AllToasts).Returns(new ToastSettings[] 
			{ 
				new ToastSettings() { IsVisible = true }, 
				new ToastSettings() { IsVisible = false },
				new ToastSettings() { IsVisible = false }
			});
			_toastServiceMock.SetupGet(g => g.GlobalSettings).Returns(new ToastContainerGlobalSettings());

			var rendered = _testContext.RenderComponent<ToastContainer>();

			var div = rendered.Find("div");
			Assert.IsNotNull(div);

			rendered.WaitForAssertion(() => rendered.MarkupMatches(@"<div style=""max-width: 400px; width: 400px;"" class=""btoast-container position-topright"" >
			  <div class=""btoast-main bnotify-normal-primary"" style=""opacity: 1; box-shadow: 1px 5px 20px 0px #c7c7c7;"" tabindex=""1000""  >
				<div class=""btoast-body"" >
				  <div >
					<svg class=""btoast-img"" focusable=""false"" viewBox=""0 0 24 24"" aria-hidden=""true"" >
					  <path fill-rule=""evenodd"" clip-rule=""evenodd"" d=""m18.598 2.865c0-0.552-0.447-1-1-1s-1 0.448-1 1v2c0 0.552 0.447 1 1 1s1-0.448 1-1v-2zm-12 2c0 0.552-0.447 1-1 1s-1-0.448-1-1v-2c0-0.552 0.447-1 1-1s1 0.448 1 1v2zm13 5v10h-16v-10h16zm2-6h-2v1c0 1.103-0.897 2-2 2s-2-0.897-2-2v-1h-8v1c0 1.103-0.897 2-2 2s-2-0.897-2-2v-1h-2v18h20v-18zm-13 10h-2v-2h2v2zm4 0h-2v-2h2v2zm4 0h-2v-2h2v2zm-8 4h-2v-2h2v2zm4 0h-2v-2h2v2zm4 0h-2v-2h2v2z"" ></path>
					</svg>
				  </div>
				  <div class=""btoast-text"" ></div>
				  <button type=""button""  class=""close normal"" >
					<span aria-hidden=""true"" >&times;</span>
					<span class=""sr-only"" >Close</span>
				  </button>
				</div>
				<div class=""btoast-progress primary start"" style=""transition: width 10s linear;"" ></div>
			  </div>
			</div>"));
		}

		[TestMethod]
		public void ToastContainer_should_not_render_multiple_Visible_Toasts()
		{
			_toastInternalsMock.SetupGet(g => g.AllToasts).Returns(new ToastSettings[]
			{
				new ToastSettings() { IsVisible = true },
				new ToastSettings() { IsVisible = false },
				new ToastSettings() { IsVisible = true }
			});
			_toastServiceMock.SetupGet(g => g.GlobalSettings).Returns(new ToastContainerGlobalSettings());

			var rendered = _testContext.RenderComponent<ToastContainer>();

			var div = rendered.Find("div");
			Assert.IsNotNull(div);

			rendered.WaitForAssertion(() => rendered.MarkupMatches(@"<div style=""max-width: 400px; width: 400px;"" class=""btoast-container position-topright"" >
			  <div class=""btoast-main bnotify-normal-primary"" style=""opacity: 1; box-shadow: 1px 5px 20px 0px #c7c7c7; margin-bottom: 17px;"" tabindex=""1000""  >
				<div class=""btoast-body"" >
				  <div >
					<svg class=""btoast-img"" focusable=""false"" viewBox=""0 0 24 24"" aria-hidden=""true"" >
					  <path fill-rule=""evenodd"" clip-rule=""evenodd"" d=""m18.598 2.865c0-0.552-0.447-1-1-1s-1 0.448-1 1v2c0 0.552 0.447 1 1 1s1-0.448 1-1v-2zm-12 2c0 0.552-0.447 1-1 1s-1-0.448-1-1v-2c0-0.552 0.447-1 1-1s1 0.448 1 1v2zm13 5v10h-16v-10h16zm2-6h-2v1c0 1.103-0.897 2-2 2s-2-0.897-2-2v-1h-8v1c0 1.103-0.897 2-2 2s-2-0.897-2-2v-1h-2v18h20v-18zm-13 10h-2v-2h2v2zm4 0h-2v-2h2v2zm4 0h-2v-2h2v2zm-8 4h-2v-2h2v2zm4 0h-2v-2h2v2zm4 0h-2v-2h2v2z"" ></path>
					</svg>
				  </div>
				  <div class=""btoast-text"" ></div>
				  <button type=""button""  class=""close normal"" >
					<span aria-hidden=""true"" >&times;</span>
					<span class=""sr-only"" >Close</span>
				  </button>
				</div>
				<div class=""btoast-progress primary start"" style=""transition: width 10s linear;"" ></div>
			  </div>
			  <div class=""btoast-main bnotify-normal-primary"" style=""opacity: 1; box-shadow: 1px 5px 20px 0px #c7c7c7;"" tabindex=""1000""  >
				<div class=""btoast-body"" >
				  <div >
					<svg class=""btoast-img"" focusable=""false"" viewBox=""0 0 24 24"" aria-hidden=""true"" >
					  <path fill-rule=""evenodd"" clip-rule=""evenodd"" d=""m18.598 2.865c0-0.552-0.447-1-1-1s-1 0.448-1 1v2c0 0.552 0.447 1 1 1s1-0.448 1-1v-2zm-12 2c0 0.552-0.447 1-1 1s-1-0.448-1-1v-2c0-0.552 0.447-1 1-1s1 0.448 1 1v2zm13 5v10h-16v-10h16zm2-6h-2v1c0 1.103-0.897 2-2 2s-2-0.897-2-2v-1h-8v1c0 1.103-0.897 2-2 2s-2-0.897-2-2v-1h-2v18h20v-18zm-13 10h-2v-2h2v2zm4 0h-2v-2h2v2zm4 0h-2v-2h2v2zm-8 4h-2v-2h2v2zm4 0h-2v-2h2v2zm4 0h-2v-2h2v2z"" ></path>
					</svg>
				  </div>
				  <div class=""btoast-text"" ></div>
				  <button type=""button""  class=""close normal"" >
					<span aria-hidden=""true"" >&times;</span>
					<span class=""sr-only"" >Close</span>
				  </button>
				</div>
				<div class=""btoast-progress primary start"" style=""transition: width 10s linear;"" ></div>
			  </div>
			</div>"));
		}

		[TestMethod]
		public void ToastContainer_should_render_Toast_with_Settings()
		{
			_toastInternalsMock.SetupGet(g => g.AllToasts).Returns(new ToastSettings[] { new ToastSettings() 
				{ 
					ShowIcon = false,
					NotificationStyle = NotificationStyles.Outlined,
					ShowCloseCountdownProgress = false,
					ShowCloseButton = false
				} 
			});
			_toastServiceMock.SetupGet(g => g.GlobalSettings).Returns(new ToastContainerGlobalSettings() { });

			var rendered = _testContext.RenderComponent<ToastContainer>();

			var div = rendered.Find("div");
			Assert.IsNotNull(div);

			rendered.WaitForAssertion(() => rendered.MarkupMatches(@"<div style=""max-width: 400px; width: 400px;"" class=""btoast-container position-topright"" >
			  <div class=""btoast-main bnotify-outlined-primary"" style=""opacity: 1; box-shadow: 1px 5px 20px 0px #c7c7c7;"" tabindex=""1000""  >
				<div class=""btoast-body"" >
				  <div class=""btoast-text"" ></div>
			  </div>
			</div>"));
		}

		[ExpectedException(typeof(ApplicationException))]
		[TestMethod]
		public void ToastContainer_should_not_rendered_mulitple_instances()
		{
			var rendered = _testContext.RenderComponent<ToastContainer>();

			var rendered2 = _testContext.RenderComponent<ToastContainer>();
		}
	}
}